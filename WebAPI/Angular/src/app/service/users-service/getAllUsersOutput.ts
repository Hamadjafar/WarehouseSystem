import { RoleDto } from "./roleDto";
import { UserDto } from "./userDto";

export class GetAllUsersOutput extends UserDto {
    role: RoleDto;
}