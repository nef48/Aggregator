export function IsNullOrUndefined(value) {
    return (value === undefined || value === null);
}

export function IsNullOrEmpty(value) {
    return (IsNullOrUndefined(value) || value === "");
}