import axios from "axios";

const API_URL = "http://localhost:5000"; // поменяй на свой адрес, если нужно

export async function getUsers() {
  const response = await axios.get(`${API_URL}/admin/users`);
  return response.data;
}

export async function banUser(userId) {
  await axios.post(`${API_URL}/admin/ban/user/${userId}`);
}

export async function unbanUser(userId) {
  await axios.post(`${API_URL}/admin/unban/user/${userId}`);
}
