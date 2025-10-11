import axios from 'axios';

const API_URL = process.env.NEXT_PUBLIC_API_URL;
const client = axios.create({
    baseURL: API_URL
});

export async function post(url: string, data: object) {
    try {
        const response = await client.post(url, data);
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
