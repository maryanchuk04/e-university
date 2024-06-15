export interface AuthModel {
    email: string;
    picture: string;
    fullName: string;
}

export interface AuthTokens {
    refreshToken: string;
    accessToken: string;
}
