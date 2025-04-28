import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import Header from "../components/header/Header";
import CustomButton from "../components/common/CustomButton";
import getLongreadsByModuleId from "../services/getLongreadsByModuleId";
import getBooks from "../services/getBooks";

const longreadColors = [
    'bg-[#0f9fff]',
    'bg-[#3ebfff]',
    'bg-[#6fcdfc]',
];

const ModulePage = function () {
    const { courseId, moduleId } = useParams();
    const navigate = useNavigate();
    const [longreads, setLongreads] = useState([]);
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const location = useLocation();
    const { moduleTitle } = location.state || {};

    useEffect(() => {
        async function fetchData() {
            try {
                const [longreadsData, booksData] = await Promise.all([
                    getLongreadsByModuleId(moduleId),
                    getBooks(moduleId),
                ]);

                setLongreads(longreadsData);
                setBooks(booksData);
            } catch (err) {
                console.error(err);
                setError('Не удалось загрузить данные');
            } finally {
                setLoading(false);
            }
        }

        fetchData();
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
            <div className="max-w-2xl mx-auto p-6 mt-4 bg-[#f7f8fc] pt-8">
                <h1 className="text-2xl font-bold mb-1 text-center">Блок {moduleId}</h1>
                <h1 className="text-2xl font-bold mb-6 text-center">{moduleTitle || "Тайм-менеджмент"}</h1>

                <h2 className="text-lg font-semibold mb-4 text-gray-700 border-b pb-2">Введение в тему</h2>

                <div className="flex flex-col items-center space-y-4 mb-6">
                    {longreads.map((item, index) => (
                        <CustomButton
                            key={item.id}
                            text={item.title}
                            className={`${longreadColors[index % longreadColors.length]} w-full max-w-[300px]`}
                            onClick={() => navigate(`/longreads/${item.id}`)}
                        />
                    ))}
                </div>

                <div className="text-center mt-2 mb-6">
                    <button
                        className="w-full max-w-[120px] bg-[#0EAA67] text-white font-semibold py-2 rounded-xl border border-[#0EAA67]"
                        onClick={() => navigate(`/courses/${courseId}/tests/${moduleId}`)}
                    >
                        Пройти тест
                    </button>
                </div>


                <h3 className="text-lg font-semibold mb-4 text-gray-700 border-b pb-2">Список рекомендуемых книг</h3>

                <div className="space-y-4">
                    {books.map((book) => (
                        <div
                            key={book.id}
                            className="flex items-start space-x-4 p-4 rounded-md border border-gray-300 bg-white hover:bg-gray-50 transition-colors"
                        >
                            <img
                                src={book.coverUrl}
                                alt={`Обложка книги ${book.author}`}
                                className="w-14 h-20 object-cover rounded-md border border-gray-300"
                            />
                            <div className="text-left">
                                <div className="font-semibold text-gray-800">{book.author}</div>
                                <div className="text-sm text-gray-700">{book.title}</div>
                                <a
                                    href={book.contentUrl}
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="block mt-2 text-blue-600 hover:underline text-sm"
                                >
                                    Скачать PDF
                                </a>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
};

export default ModulePage;
