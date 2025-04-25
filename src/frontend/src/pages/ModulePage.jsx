import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import CustomButton from "../components/common/CustomButton";

const longreads = [
    { title: 'Тайм-менеджмент тим-лида', link: '/longreads/time-management' },
    { title: 'Матрица Эйзенхауэра', link: '/longreads/eisenhower-matrix' },
    { title: 'Метод ABCDE', link: '/longreads/abcde-method' },
];

const recommendedBooks = [
    {
        author: 'Автор автор',
        description: 'Описание книги 1',
        coverUri: 'https://minio.example.com/covers/book1.jpg',
    },
    {
        author: 'Автор',
        description: 'Описание книги 2',
        coverUri: 'https://minio.example.com/covers/book2.jpg',
    },
    {
        author: 'Автор',
        description: 'Описание книги 3',
        coverUri: 'https://minio.example.com/covers/book3.jpg',
    },
];


const buttonColors = [
    'bg-[#0f9fff]',
    'bg-[#3ebfff]',
    'bg-[#fec810]',
    'bg-[#f87c14]',
];

const ModulePage = function () {
    const { moduleId } = useParams();
    const navigate = useNavigate();

    return (
        <div className="max-w-2xl mx-auto p-6 text-center">
            <h1 className="text-2xl font-bold mb-6">Основы личной эффективности тим-лида</h1>
            <h2 className="text-left text-lg font-semibold mb-4">Введение в тему</h2>

            <div className="space-y-3 mb-4 text-left">
                {longreads.map((item, index) => (
                    <CustomButton
                        key={index}
                        text={`${item.title}`}
                        className={`${buttonColors[index % buttonColors.length]}`}
                        onClick={() => navigate(item.link)}
                    >
                        {item.title}
                    </CustomButton>
                ))}
            </div>

            <button
                className="bg-white border border-gray-300 rounded px-4 py-2 mb-6 hover:bg-gray-100"
                onClick={() => navigate(`/tests/${moduleId}`)}>
                Пройти тест
            </button>

            <h2 className="text-left text-lg font-semibold mb-4">Список рекомендуемых книг</h2>
            <div className="space-y-4">
                {recommendedBooks.map((book, index) => (
                    <div key={index} className="flex items-start space-x-4 border p-4 rounded-lg bg-gray-50">
                        <img
                            src={book.coverUri}
                            alt={`Обложка книги ${book.author}`}
                            className="w-12 h-16 object-cover rounded"
                        />
                        <div className="text-left">
                            <div className="font-semibold">{book.author}</div>
                            <div className="text-sm text-gray-600">{book.description}</div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ModulePage;
