import { useEffect } from "react";

const Modal = ({ isOpen, onClose, title, message }) => {
    useEffect(() => {
        if (isOpen) {
            document.body.style.overflow = "hidden";
        } else {
            document.body.style.overflow = "auto";
        }
    }, [isOpen]);

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-40 backdrop-blur-sm animate-fade-in">
            <div className="bg-white rounded-2xl shadow-2xl p-6 mx-4 sm:mx-0 max-w-sm w-full text-center animate-slide-up">
                <h2 className="text-xl font-semibold text-gray-800 mb-3">{title}</h2>
                <p className="text-gray-600 mb-5">{message}</p>
                <button
                    className="px-5 py-2 bg-blue-500 hover:bg-blue-600 text-white font-medium rounded-xl transition-all"
                    onClick={onClose}
                >
                    Понятно
                </button>
            </div>
        </div>
    );
};

export default Modal;
