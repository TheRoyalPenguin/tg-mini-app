import React from "react";
import CustomInput from "./CustomInput";
import CustomButton from "./CustomButton";

const AuthPage = function () {
  return (
    <div className="flex flex-col items-center h-screen text-center bg-[#f7f8fc]">
      <img
        src="/images/universal_element_1.png"
        className="w-[160px] mt-[5%]"
        alt="universal element"
      />
      <p className="font-sans font-bold text-[26px] mt-[10px] mb-[15px]">
        Добро пожаловать <br /> на образовательную <br /> площадку БАРС Груп!
      </p>
      <CustomInput
        type="text"
        placeholder="Фамилия"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
        />
        <CustomInput
        type="text"
        placeholder="Имя"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
        />
        <CustomInput
        type="text"
        placeholder="Отчество"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
        />
        <CustomButton
        text="Подтвердить телефон"
        className="mt-[13px] bg-[#f87c14] w-[300px] h-[50px] rounded-[15px] text-[18px] text-white hover:opacity-70"
        />
    </div>
  );
};

export default AuthPage;
