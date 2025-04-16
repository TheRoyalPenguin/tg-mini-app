import axios from "axios";

export const fetchTest = async () => {
    try {
        const response = await axios.get('https://levelupapp.hopto.org:443/api/test');
        console.log(response.data);
    } catch (e) {
        console.error(e);
    }
};