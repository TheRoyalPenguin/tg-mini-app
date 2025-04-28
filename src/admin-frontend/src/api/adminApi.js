import axios from "axios";

const API_URL = "http://localhost:5000"; // поменяй на свой адрес сервера

export const banUser = async (userId) => {
  await axios.post(`${API_URL}/ban/user/${userId}`);
};

export const unbanUser = async (userId) => {
  await axios.post(`${API_URL}/unban/user/${userId}`);
};