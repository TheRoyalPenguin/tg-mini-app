import axios from "axios";

export const Testfetch = async () => {
    try{
        let response = await axios.get('http://localhost:48348/api/test');
        console.log(response);
    }
    catch (e){
        console.error(e);
    }
};