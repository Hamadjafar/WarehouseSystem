import { GetAllUsersOutput } from "./getAllUsersOutput";

export class UsersOutputDto {
    userDto: GetAllUsersOutput[];
    totalItems: number;
}