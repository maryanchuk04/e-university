import axios from 'axios';

export const httpClient = axios.create({
    baseURL: process.env.GATEWAY_BASE_ADDRESS,
    withCredentials: true,
});
