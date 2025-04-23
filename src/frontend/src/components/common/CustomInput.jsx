import React from "react";

const CustomInput = function(props) {
    return (
        <input
            type={props.type}
            placeholder={props.placeholder}
            className={props.className}
        />
    );
}

export default CustomInput;
