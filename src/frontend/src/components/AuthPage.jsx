import React from "react";
import CustomInput from "./CustomInput";
import CustomButton from "./CustomButton";
import '../styles/AuthPage.css'

const AuthPage = function() {
    return (
        <div className="AuthPage">
            <img src="/images/universal_element_1.png" className="universal_element"></img>
            <p className="title">Добро пожаловать <br></br> на образовательную <br></br> площадку БАРС Груп!</p>
            <CustomInput type={"text"} placeholder={"Фамилия"} color={"#f0f0f8"}></CustomInput>
            <CustomInput type={"text"} placeholder={"Имя"} color={"#f0f0f8"}></CustomInput>
            <CustomInput type={"text"} placeholder={"Отчество"} color={"#f0f0f8"}></CustomInput>
            <CustomButton text={"Подтвердить телефон"} color={"#f87c14"}></CustomButton>
        </div>
    )
}

export default AuthPage;