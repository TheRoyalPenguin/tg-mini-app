function LongreadAudio({ audioUrl }) {
    return (
        <div className="text-center my-8">
            <audio controls src={audioUrl} className="mx-auto">
                Ваш браузер не поддерживает воспроизведение аудио.
            </audio>
        </div>
    );
}

export default LongreadAudio;
