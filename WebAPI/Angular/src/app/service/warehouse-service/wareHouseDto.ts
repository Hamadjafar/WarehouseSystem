import { WareHouseItemsDto } from "./wareHouseItemsDto";

export class WareHouseDto {
  id: number;
  name: string;
  address: string;
  city: string;
  countryId: number;
  countryName: string;
  wareHouseItemsDto: WareHouseItemsDto[] = [];
}