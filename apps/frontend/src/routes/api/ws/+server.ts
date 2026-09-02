const ws = new WebSocket('wss://YOUR-DOMAIN.vercel.app/api/ws');

ws.onopen = () => {
	console.log('connected');
	ws.send('hello');
};

ws.onmessage = (event) => {
	console.log('received:', event.data);
};


ws.onclose = () => {
	console.log('closed');
};
