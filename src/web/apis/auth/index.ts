import { AuthenticateRequest } from '@/core/models/authenticate';
import { httpClient } from '../http';
import { AxiosResponse } from 'axios';

const authenticateRoute = 'authenticate';

export const authenticate = async (authRequest: AuthenticateRequest) => {
    try {
        const res = await httpClient.post<AuthenticateRequest, AxiosResponse<AuthenticateRequest>>(authenticateRoute, authRequest);

        return res.data;
    } catch (error) {
        console.error(`Error happened during authenticate user = ${authRequest.email}`, error);
        throw error;
    }
};

export const logout = async () => {
    try {
        await httpClient.post(`${authenticateRoute}/logout`);
    } catch (error) {
        console.error(`Error happened during logout user`, error);
        throw error;
    }
};
