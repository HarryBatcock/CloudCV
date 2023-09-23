window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
  });
  
  const functionApi = 'https://cvsitefunction.azurewebsites.net/api/GetCVCounter?code=V1nNRCEYhmBayN5XwkXmUFg8psWcvPnTNYKFyAkjnvpwAzFudnOitw==';
  const localfunctionApi = 'http://localhost:7071/api/GetCVCounter';

  const getVisitCount = async () => {
    try {
      const response = await fetch(functionApi);
      const jsonResponse = await response.json();
      const count = jsonResponse.count;
      document.getElementById('counter').innerHTML = count;
    } catch (error) {
      console.log(error);
    }
  };