function ReadConfirmationButton() {
    return (
        <div className="text-center mt-10">
            <button
                onClick={() => alert("Отлично!")}
                className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 px-6 rounded-lg transition"
            >
                Я прочитал(а)
            </button>
        </div>
    );
}

export default ReadConfirmationButton;
