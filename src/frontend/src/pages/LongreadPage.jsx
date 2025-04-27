import { useEffect, useState } from "react";
import getLongreadById from "../services/getLongreadById";
import { useParams } from "react-router-dom";

function LongreadPage() {
    const { longreadId } = useParams();
    const [longread, setLongread] = useState(null);
    const [htmlContent, setHtmlContent] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchLongread() {
            try {
                const data = await getLongreadById(longreadId);
                setLongread(data);

                const htmlResponse = await fetch(data.htmlUrl);
                const htmlText = await htmlResponse.text();

                // Оставляем только body контент
                const parser = new DOMParser();
                const doc = parser.parseFromString(htmlText, "text/html");
                const bodyContent = doc.body.innerHTML;

                setHtmlContent(bodyContent);
            } catch (err) {
                setError('Не удалось загрузить лонгрид');
                console.error(err);
            } finally {
                setLoading(false);
            }
        }

        fetchLongread();
    }, [longreadId]);

    if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
    if (error) return <div className="text-center mt-10 text-red-500">{error}</div>;
    if (!longread) return <div className="text-center mt-10">Нет данных</div>;

    return (
        <div className="max-w-3xl mx-auto px-4 py-8">
            {/* Заголовок */}
            <h1 className="text-4xl font-bold mb-4 text-center text-gray-800">{longread.title}</h1>

            {/* Описание */}
            <p className="text-lg text-gray-600 mb-8 text-center">{longread.description}</p>

            {/* Чистый текст без панели */}
            <div
                dangerouslySetInnerHTML={{ __html: htmlContent }}
                className="mb-10 leading-relaxed text-gray-800"
            />

            {/* Кнопка */}
            <div className="text-center">
                <button
                    onClick={() => alert("Отлично!")}
                    className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 px-6 rounded-lg transition"
                >
                    Я прочитал(а)
                </button>
            </div>
        </div>
    );
}

export default LongreadPage;
