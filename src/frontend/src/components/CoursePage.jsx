import React from "react";
import CustomButton from "./CustomButton";
import '../styles/CoursePage.css'

const CoursePage = function() {
    return (
        <div className="CoursePage">
            <img src="/images/universal_element_3.png" className="universal_element_coursePage"></img>
            <p className="title_coursePage">Готов прокачать <br></br>навыки Тим Лида?</p>
            <p className="text_coursePage">Выберите тему, чтобы начать<br></br>обучение. После прохождения<br></br>каждой темы - краткий тест.<br></br>Удачи!</p>
            <CustomButton text={"Блок 1"} color={"#0f9fff"}></CustomButton>
            <CustomButton text={"Блок 2"} color={"#3ebfff"}></CustomButton>
            <CustomButton text={"Блок 3"} color={"#fec810"}></CustomButton>
            <CustomButton text={"Блок 4"} color={"#f87c14"}></CustomButton>
        </div>
    )
}

export default CoursePage;