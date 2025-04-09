import axios from "axios";

export const fetchTest = async () => {
    try{
        let response = await axios.get('/api/test');
        console.log(response);
    }
    catch (e){
        console.error(e);
    }
};