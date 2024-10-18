export class LogsDto {
    timestamp: Date;
    level: string;
    exception: string;
    renderedMessage: string;
    properties: string;
}