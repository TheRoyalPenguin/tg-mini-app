using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class LongreadService : ILongreadService
{
    private readonly ILongreadRepository _repository;
    private readonly IModuleRepository _moduleRepository;
    private readonly ILongreadConverter _converter;
    private readonly IUnitOfWork _uow;

    public LongreadService(
        ILongreadRepository repository,
        ILongreadConverter converter,
        IModuleRepository moduleRepository,
        IUnitOfWork uow)
    {
        _repository = repository;
        _converter = converter;
        _moduleRepository = moduleRepository;
        _uow = uow;
    }

    public async Task<Result<Longread>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var longreadResult = await _repository.GetByIdAsync(id, ct);

        if (!longreadResult.IsSuccess)
        {
            return Result<Longread>.Failure(longreadResult.ErrorMessage);
        }

        return Result<Longread>.Success(longreadResult.Data)!;
    }

    public async Task<Result<IReadOnlyList<Longread>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        var longreadsResult = await _repository.ListByModuleAsync(moduleId, ct);

        if (!longreadsResult.IsSuccess)
        {
            return Result<IReadOnlyList<Longread>>.Failure(longreadsResult.ErrorMessage);
        }

        return Result<IReadOnlyList<Longread>>.Success(longreadsResult.Data)!;
    }

    public async Task<Result<int>> AddAsync(int moduleId, CreateLongreadModel model, CancellationToken ct = default)
    {
        await _uow.StartTransactionAsync();

        try
        {
            var conv = await _converter.ConvertAsync(
                model.DocxStream,
                model.DocxFileName,
                model.AudioStream,
                model.AudioFileName,
                model.ModuleId,
                ct
            );

            var longread = new Longread
            {
                ModuleId = model.ModuleId,
                Title = model.Title,
                Description = model.Description,
                OriginalDocxKey = conv.OriginalDocxKey,
                HtmlContentKey = conv.HtmlKey,
                AudioContentKey = conv.AudioKey,
                ImageKeys = conv.ImageKeys.ToList()
            };

            var result = await _repository.AddAsync(longread, ct);

            if (!result.IsSuccess)
            {
                await _uow.RollbackTransactionAsync();
                return Result<int>.Failure(result.ErrorMessage);
            }

            var moduleGetResult = await _moduleRepository.GetByIdAsync(moduleId);
            if (!moduleGetResult.IsSuccess)
            {
                await _uow.RollbackTransactionAsync();
                return Result<int>.Failure(result.ErrorMessage);
            }

            var module = moduleGetResult.Data;
            if (module == null)
            {
                return Result<int>.Failure("Не удалось найти модуль с указанным id=" + moduleId);
            }

            module.LongreadCount++;

            var moduleUpdateResult = await _moduleRepository.UpdateAsync(module);
            if (!moduleUpdateResult.IsSuccess)
            {
                await _uow.RollbackTransactionAsync();
                return Result<int>.Failure(result.ErrorMessage);
            }

            await _uow.CommitTransactionAsync();
            return Result<int>.Success(longread.Id);
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            return Result<int>.Failure($"Ошибка при создании лонгрида: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Longread longread, CancellationToken ct = default)
    {
        var result = await _repository.UpdateAsync(longread, ct);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var result = await _repository.DeleteAsync(id, ct);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> MarkAsReadAsync(int longreadId, int userId, int moduleId, CancellationToken ct = default)
    {
        return await _repository.MarkAsReadAsync(longreadId, userId, moduleId, ct);
    }
}
