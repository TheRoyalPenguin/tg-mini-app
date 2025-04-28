import { setLongreadCompletion } from "../../services/setLongreadCompletion";

function ReadConfirmationButton({ longreadId, moduleId }) {
    const handleClick = async () => {
        try {
            await setLongreadCompletion(longreadId, moduleId);
            alert("Отлично! Прочтение зафиксировано ✅");
        } catch (error) {
            alert(error.message);
        }
    };

    return (
        <div className="text-center mt-10">
            <button
                onClick={handleClick}
                className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 px-6 rounded-lg transition"
            >
                Я прочитал(а)
            </button>
        </div>
    );
}

export default ReadConfirmationButton;
