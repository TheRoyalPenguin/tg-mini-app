import { useState } from 'react';
import Header from "../components/header/Header";

const faqData = [
    {
        question: 'Как я могу связаться с поддержкой?',
        answer: 'Вы можете связаться с нашей службой поддержки через форму на странице "Поддержка" или отправить письмо на почту support@example.com.'
    },
    {
        question: 'Где найти информацию о доступных курсах?',
        answer: 'Перейдите на главную страницу после входа в аккаунт — там вы найдете список всех доступных курсов.'
    },
    {
        question: 'Почему некоторые модули недоступны?',
        answer: 'Модуль может быть недоступен, потому что вы еще не открыли его. Чтобы открывать новые модули, вам необходимо сначала пройти тест предыдущего модуля.'
    },
    {
        question: 'Что делать, если меня несправедливо забанили?',
        answer: 'Обратитесь в нашу службу поддержки через форму на странице "Поддержка" и мы обязательно разберемся с вашей ситуацией и напишем вам!'
    },

];

const FAQPage = () => {
    const [activeIndex, setActiveIndex] = useState(null);

    const toggleQuestion = (index) => {
        setActiveIndex(prevIndex => prevIndex === index ? null : index);
    };

    return (
        <>
            <Header/>
        <div className="flex flex-col items-center min-h-screen bg-[#f7f8fc] p-4">
            <img
                src="/images/box.png"
                className="w-[200px] mt-[20px]"
                alt="FAQ"
            />
            <p className="font-sans mt-[10px] mb-[7px] font-bold text-[28px] text-center">
                Часто задаваемые вопросы
            </p>
            <div className="w-full max-w-[600px] mt-6 flex flex-col gap-4">
                {faqData.map((item, index) => (
                    <div
                        key={index}
                        className="border border-gray-400 rounded-[12px] bg-white p-4"
                    >
                        <button
                            onClick={() => toggleQuestion(index)}
                            className="w-full flex justify-between items-center text-left text-[18px] font-semibold"
                        >
                            <span>{item.question}</span>
                            <span className="text-[24px] text-blue-700">
                                {activeIndex === index ? '-' : '+'}
                            </span>
                        </button>
                        {activeIndex === index && (
                            <p className="mt-3 text-[16px] text-gray-600">
                                {item.answer}
                            </p>
                        )}
                    </div>
                ))}
            </div>
        </div>
        </>
    );
};

export default FAQPage;
