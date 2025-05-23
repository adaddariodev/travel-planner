export const login = async (email: string, password: string) => {
  const res = await fetch('http://localhost:5226/api/auth/login', {
    method: 'POST',
    credentials: 'include', // 👈 Obbligatorio per i cookie
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
  });
  return await res.json();
};

// Stessa modifica per register/logout