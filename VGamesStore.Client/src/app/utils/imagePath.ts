export function imagePath(imageUrl : any | null): string | null {
    const baseUrl = 'https://localhost:7229'; // Change to match your API URL
    return imageUrl ? `${baseUrl}${imageUrl}` : null;
  }
