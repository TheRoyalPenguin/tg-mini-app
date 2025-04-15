import React from "react";
import CustomButton from "./CustomButton";

const WelcomePage = function() {
    return (
        <div className="flex flex-col items-center h-screen text-center bg-[#f7f8fc]">
            <img 
                src="/images/universal_element_2.png" 
                className="w-[200px] mt-[20px]" 
                alt="universal_element"
            />
            <p className="font-sans mt-[10px] mb-[7px] font-bold text-[33px]">
                Привет, <br />Телеграм ID!
            </p>
            <p className="font-sans text-[20px]">
                Выберите курс, чтобы <br />скорее приступить <br />к обучению!
            </p>
            <CustomButton text="Team lead" className="bg-[#89d018] rounded-[15px] text-[18px]" />
            <CustomButton text="Frontend" className="bg-[#0793fe] rounded-[15px] text-[18px]" />
            <CustomButton text="PM" className="bg-[#f87c14] rounded-[15px] text-[18px]" />
        </div>
    );
}

export default WelcomePage;
