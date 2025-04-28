import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Header from "../components/header/Header";
import CustomButton from "../components/common/CustomButton";
import getLongreadsByModuleId from "../services/getLongreadsByModuleId";

const recommendedBooks = [
    {
        author: 'Автор автор',
        description: 'Описание книги 1',
        coverUri: '',
    },
    {
        author: 'Автор',
        description: 'Описание книги 2',
        coverUri: '',
    },
    {
        author: 'Автор',
        description: 'Описание книги 3',
        coverUri: '',
    },
];

const longreadColors = [
    'bg-[#0f9fff]',
    'bg-[#3ebfff]',
    'bg-[#6fcdfc]',
];

const bookColors = [
    'bg-[#f87c14]',
    'bg-[#f89614]',
    'bg-[#fec810]',
];

const ModulePage = function () {
    const { moduleId } = useParams();
    const navigate = useNavigate();
    const [longreads, setLongreads] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchLongreads() {
            try {
                const data = await getLongreadsByModuleId(moduleId);
                console.log(data);
                setLongreads(data);
            } catch (err) {
                setError('Не удалось загрузить лонгриды');
            } finally {
                setLoading(false);
            }
        }

        fetchLongreads();
    }, [moduleId]);

    if (loading) {
        return (
            <>
                <Header />
                <div className="max-w-2xl mx-auto p-6 mt-4 text-center">
                    Загрузка...
                </div>
            </>
        );
    }

    if (error) {
        return (
            <>
                <Header />
                <div className="max-w-2xl mx-auto p-6 mt-4 text-center text-red-500">
                    {error}
                </div>
            </>
        );
    }

    return (
        <>
            <Header />
            <div className="max-w-2xl mx-auto p-6 mt-4 bg-[#f7f8fc]  pt-8">
                <h1 className="text-2xl font-bold mb-1 text-center">Блок 1</h1>
                <h1 className="text-2xl font-bold mb-6 text-center">"Тайм-менеджмент"</h1>

                <h2 className="text-left text-lg font-semibold mb-2 text-gray-700">Введение в тему</h2>

                <div className="flex flex-col items-center space-y-4 mb-6">
                    {longreads.map((item, index) => (
                        <CustomButton
                            key={index}
                            text={item.title}
                            className={`${longreadColors[index % longreadColors.length]} w-full max-w-[300px]`}
                            onClick={() => navigate(`/longreads/${item.id}`)}
                        />
                    ))}
                </div>

                <div className="text-center mb-6">
                    <button
                        className="bg-[#0EAA67] border border-gray-300 rounded px-4 py-2 hover:bg-gray-100 mt-1 mb-1"
                        onClick={() => navigate(`/tests/${moduleId}`)}
                    >
                        Пройти тест
                    </button>
                </div>

                <h2 className="text-left text-lg font-semibold mb-4 text-gray-700">Список рекомендуемых книг</h2>
                <div className="space-y-4">
                    {recommendedBooks.map((book, index) => (
                        <div
                            key={index}
                            className={`flex items-start space-x-4 p-4 rounded-lg ${bookColors[index % bookColors.length]}`}
                        >
                            <img
                                src={book.coverUri}
                                alt={`Обложка книги ${book.author}`}
                                className="w-12 h-16 object-cover rounded"
                            />
                            <div className="text-left">
                                <div className="font-semibold text-white">{book.author}</div>
                                <div className="text-sm text-white">{book.description}</div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
};

export default ModulePage;
