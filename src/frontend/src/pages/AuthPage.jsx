import React, { useEffect, useState } from "react";
import axios from "axios";
import CustomInput from "../components/common/CustomInput";
import CustomButton from "../components/common/CustomButton";

const AuthPage = () => {
  const botLink = "https://t.me/LevelUpAppBot?start=confirmPhone";
  const [initData, setInitData] = useState("");

  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [patronymic, setPatronymic] = useState("");

  useEffect(() => {
    if (window.Telegram?.WebApp) {
      const tg = window.Telegram.WebApp;

      setInitData(tg.initData);

      // tg.initDataUnsafe – объект с данными пользователя, получаем его и сохраняем в состоянии
      const data = tg.initDataUnsafe;

      if (data) {
        setName(data.first_name || "");
        setSurname(data.last_name || "");
      }
    }
  }, []);

  const handleAuth = async () => {
    if (initData) {
      try {
        const payload = {
          initData: initData,
          name: name,
          surname: surname,
          patronymic: patronymic,
        };

        const response = await axios.post(
          "http://localhost:5090/api/auth/telegram-mini-app",
          payload
        );
        console.log("Ответ от сервера:", response.data);
      } catch (error) {
        console.error("Ошибка при авторизации:", error);
      }
    }
  };

  const handleConfirmClick = () => {
    if (window.Telegram?.WebApp) {
      window.Telegram.WebApp.openLink(botLink);
    } else {
      window.location.href = botLink;
    }
  };

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
        value={surname}
        onChange={(e) => setSurname(e.target.value)}
        placeholder="Фамилия"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
      />
      <CustomInput
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder="Имя"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
      />
      <CustomInput
        type="text"
        value={patronymic}
        onChange={(e) => setPatronymic(e.target.value)}
        placeholder="Отчество"
        className="mt-[13px] bg-[#f0f0f8] border-none w-[300px] h-[50px] rounded-[15px] pl-[15px] text-[18px] placeholder:text-[#52555b]"
      />
      <CustomButton
        text="Подтвердить телефон"
        onClick={handleConfirmClick}
        className="mt-[13px] bg-[#f87c14] w-[300px] h-[50px] rounded-[15px] text-[18px] text-white hover:opacity-70"
      />

      {initData ? (
        <div className="mt-[20px]">
          <CustomButton
            text="Войти"
            onClick={handleAuth}
            className="mt-[10px] px-4 py-2 bg-blue-500 text-white rounded"
          />
        </div>
      ) : (
        <p>Загрузка данных...</p>
      )}
    </div>
  );
};

export default AuthPage;
