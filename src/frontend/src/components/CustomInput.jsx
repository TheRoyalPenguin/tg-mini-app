import React from "react";
import '../styles/AuthPage.css'

const CustomInput = function(props) {
    return (
        <input type={props.type} placeholder={props.placeholder} className="CustomInput" style={{backgroundColor:props.color}}></input>
    )
}

export default CustomInput;