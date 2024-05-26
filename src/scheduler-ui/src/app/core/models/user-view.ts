export interface UserView {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    course: number;
    picture: string;
    faculty: string;
    group: string;
}

export const mockUser: UserView = {
    id: "616fd49a-202b-4c4b-bc93-76bd8eb6bef1",
    email: "marianchuk.maksym@chnu.edu.ua",
    firstName: "Максим",
    lastName: "Мар`янчук",
    course: 4,
    picture: "https://lh3.googleusercontent.com/a/ACg8ocIRycwgoSwjyLWie6u0nWELeTRTi-UjSGEBV35sfTxBgti9qBeATQ=s576-c-no",
    faculty: "Факультет Математики та Інформатики",
    group: '402'
}