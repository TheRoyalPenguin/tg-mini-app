import React from "react";

const CustomButton = function (props) {
    const { text, className, onClick, disabled = false } = props;

    return (
        <button
            className={`mt-[13px] w-[300px] h-[50px] rounded-[15px] text-[18px] text-white 
                ${disabled ? 'opacity-50 cursor-not-allowed focus:outline-none' : 'hover:opacity-70'} 
                ${className}`}
            onClick={disabled ? undefined : onClick}
            disabled={disabled}
            tabIndex={disabled ? -1 : 0}
        >
            {text}
        </button>
    );
};

export default CustomButton;
