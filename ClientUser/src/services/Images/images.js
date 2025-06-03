async function fetchBlob(uri) {
    const res = await fetch(uri)
    if (!res.ok) return undefined
    return res.blob()
}

function blobToBase64(blob) {
    return new Promise((resolve, _) => {
        const reader = new FileReader();
        reader.onloadend = () => resolve(String(reader.result));
        reader.readAsDataURL(blob);
    });
}

export async function imageAsBase64(file) {
    const uri = URL.createObjectURL(file)
    const blob = await fetchBlob(uri)
    URL.revokeObjectURL(uri)
    return await blobToBase64(blob)
}