import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import getLongreadById from "../services/getLongreadById";
import LongreadHeader from "../components/longread/LongreadHeader";
import LongreadAudio from "../components/longread/LongreadAudio";
import LongreadDownload from "../components/longread/LongreadDownload";
import LongreadContent from "../components/longread/LongreadContent";
import ReadConfirmationButton from "../components/longread/ReadConfirmationButton";
import Header from "../components/header/Header";

function LongreadPage() {
    const { longreadId } = useParams();
    const [longread, setLongread] = useState(null);
    const [htmlContent, setHtmlContent] = useState('');
    const [styleContent, setStyleContent] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchLongread() {
            try {
                const data = await getLongreadById(longreadId);
                setLongread(data);

                const htmlResponse = await fetch(data.htmlUrl);
                const htmlText = await htmlResponse.text();

                const parser = new DOMParser();
                const doc = parser.parseFromString(htmlText, 'text/html');

                const extractedStyle = doc.querySelector('style')?.innerHTML || '';
                const bodyContent = doc.body.innerHTML || '';

                setStyleContent(extractedStyle);
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
        <>
            <Header/>
            <div className="max-w-5xl mx-auto px-4 py-8 bg-[#f7f8fc] pt-12">
                <LongreadHeader title={longread.title} description={longread.description}/>
                {longread.audioUrl && <LongreadAudio audioUrl={longread.audioUrl}/>}
                {longread.originalDocxUrl && <LongreadDownload docxUrl={longread.originalDocxUrl}/>}
                <LongreadContent styleContent={styleContent} htmlContent={htmlContent}/>
                <ReadConfirmationButton/>
            </div>
        </>

    );
}

export default LongreadPage;
