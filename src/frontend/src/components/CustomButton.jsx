import React from "react";

const CustomButton = function(props) {
    return (
        <button className={`mt-[13px] w-[300px] h-[50px] rounded-[15px] text-[18px] text-white hover:opacity-70 ${props.className}`}>
            {props.text}
        </button>
    );
}

export default CustomButton;
