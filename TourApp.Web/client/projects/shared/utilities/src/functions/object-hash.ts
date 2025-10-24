const textEncoder = new TextEncoder();

export function objectHash(obj: {}): number {
    let hash = 0;

    for (let value of Object.values(obj)) {
        if (!value) {
            continue;
        }

        if (typeof(value) === "object") {
            hash ^= objectHash(value);
        }
        else if (typeof(value) === "string") {
            hash ^= objectHash(textEncoder.encode(value));
        }
        else {
            hash ^= Number(value);
        }
    }

    return Math.trunc(hash);
}
