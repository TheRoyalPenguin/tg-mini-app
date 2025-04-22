import axios from "axios";

export const fetchTest = async () => {
    try {
        const response = await axios.get('http://localhost:5000/api/test');
        console.log(response.data);
    } catch (e) {
        console.error(e);
    }
};