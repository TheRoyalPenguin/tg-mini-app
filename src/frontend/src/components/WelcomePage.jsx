import React from "react";
import CustomButton from "./CustomButton";
import '../styles/WelcomePage.css'

const WelcomePage = function() {
    return (
        <div className="WelcomePage">
            <img src="/images/universal_element_2.png" className="universal_element_welcomePage"></img>
            <p className="title_welcomePage">Привет, <br></br>Телеграм ID!</p>
            <p className="text_welcomePage">Выберите курс, чтобы <br></br>скорее приступить <br></br>к обучению!</p>
            <CustomButton text={"Team lead"} color={"#89d018"}></CustomButton>
            <CustomButton text={"Frontend"} color={"#0793fe"}></CustomButton>
            <CustomButton text={"PM"} color={"#f87c14"}></CustomButton>
        </div>
    )
}

export default WelcomePage;