import { RoleDto } from "./roleDto";

export class UserDto {
    id: number;
    email: string;
    name: string;
    roleId: number;
    isActive: boolean;
}