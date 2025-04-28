import { useState } from "react";
import { setLongreadCompletion } from "../../services/setLongreadCompletion";
import Modal from "../common/Modal";

function ReadConfirmationButton({ longreadId, moduleId }) {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [modalTitle, setModalTitle] = useState("");
    const [modalMessage, setModalMessage] = useState("");

    const handleClick = async () => {
        try {
            await setLongreadCompletion(longreadId, moduleId);
            setModalTitle("Отлично!");
            setModalMessage("Прочтение зафиксировано. Продолжайте в том же духе!");
        } catch (error) {
            setModalTitle("Ошибка");
            setModalMessage(error.message || "Что-то пошло не так. Попробуйте позже.");
        } finally {
            setIsModalOpen(true);
        }
    };

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div className="text-center mt-10">
            <button
                onClick={handleClick}
                className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 px-6 rounded-lg transition"
            >
                Я прочитал(а)
            </button>

            <Modal
                isOpen={isModalOpen}
                onClose={closeModal}
                title={modalTitle}
                message={modalMessage}
            />
        </div>
    );
}

export default ReadConfirmationButton;
