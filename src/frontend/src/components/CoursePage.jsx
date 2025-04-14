import React from "react";
import CustomButton from "./CustomButton";

const CoursePage = function() {
    return (
        <div className="flex flex-col items-center justify-center h-screen text-center bg-[#f7f8fc]">
            <img 
                src="/images/universal_element_3.png" 
                className="w-[250px]" 
                alt="universal_element"
            />
            <p className="font-sans mt-[10px] font-bold text-[28px]">
                Готов прокачать <br />навыки Тим Лида?
            </p>
            <p className="font-sans text-[16px]">
                Выберите тему, чтобы начать<br />обучение. После прохождения<br />каждой темы - краткий тест.<br />Удачи!
            </p>
            <div className="flex flex-col space-y-[13px]">
                <CustomButton text="Блок 1" className="bg-[#0f9fff]" />
                <CustomButton text="Блок 2" className="bg-[#3ebfff]" />
                <CustomButton text="Блок 3" className="bg-[#fec810]" />
                <CustomButton text="Блок 4" className="bg-[#f87c14]" />
            </div>
        </div>
    );
}

export default CoursePage;
