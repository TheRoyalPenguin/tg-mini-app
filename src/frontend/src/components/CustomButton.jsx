import React from "react";
import '../styles/AuthPage.css'

const CustomButton = function(props) {
    return (
        <button style={{backgroundColor:props.color, cursor:"pointer"}} className="CustomButton">{props.text}</button>
    )
}

export default CustomButton;