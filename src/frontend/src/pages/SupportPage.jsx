import { useState } from 'react';
import CustomButton from '../components/common/CustomButton';
import {useNavigate} from "react-router-dom";

const SupportPage = () => {
    const [formData, setFormData] = useState({
        email: '',
        name: '',
        message: ''
    });

    const navigate = useNavigate();
    const [submitted, setSubmitted] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Форма отправлена:', formData);
        setSubmitted(true);
    };

    if (submitted) {
        return (
            <div className="flex flex-col items-center justify-center h-screen bg-[#f7f8fc] text-center p-4">
                <img
                    src="/images/universal_element_2.png"
                    className="w-[200px] mb-6"
                    alt="Спасибо"
                />
                <p className="text-[28px] font-bold mb-4">Спасибо за обращение!</p>
                <p className="text-[18px] mb-6">Мы скоро с вами свяжемся.</p>

                <CustomButton
                    text="На главную"
                    onClick={() => navigate('/courses')}
                    className="bg-[#89d018] rounded-[15px] text-[18px] px-6 py-2"
                />
            </div>
        );
    }

    return (
        <div className="flex flex-col items-center h-full min-h-screen text-center bg-[#f7f8fc] p-4">
            <img
                src="/images/universal_element_2.png"
                className="w-[200px] mt-[20px]"
                alt="Support"
            />
            <p className="font-sans mt-[10px] mb-[7px] font-bold text-[28px]">
                Служба поддержки
            </p>
            <p className="font-sans text-[16px] mb-6">
                Пожалуйста, заполните форму ниже
            </p>

            <form
                onSubmit={handleSubmit}
                className="flex flex-col w-full max-w-[400px] gap-4"
            >
                <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    placeholder="Ваша почта"
                    required
                    className="border border-gray-400 rounded-[10px] p-3 text-[16px]"
                />
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    placeholder="Ваше имя"
                    required
                    className="border border-gray-400 rounded-[10px] p-3 text-[16px]"
                />
                <textarea
                    name="message"
                    value={formData.message}
                    onChange={handleChange}
                    placeholder="Опишите вашу проблему или вопрос"
                    required
                    rows="5"
                    className="border border-gray-400 rounded-[10px] p-3 text-[16px] resize-none"
                />

                <div className="flex justify-center">
                    <CustomButton
                        text="Отправить сообщение"
                        type="submit"
                        className="bg-[#0793fe] rounded-[15px] text-[18px] mt-4"
                    />
                </div>
            </form>
        </div>
    );
};

export default SupportPage;
