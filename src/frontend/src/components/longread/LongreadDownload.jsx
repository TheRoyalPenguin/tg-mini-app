function LongreadDownload({ docxUrl }) {
    return (
        <div className="text-center mt-6">
            <a
                href={docxUrl}
                download
                className="bg-[#ebebeb] text-black font-semibold py-3 px-6 rounded-lg transition inline-block"
            >
                Скачать в Word
            </a>
        </div>
    );
}

export default LongreadDownload;
