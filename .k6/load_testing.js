import http from 'k6/http';
import { sleep, group } from 'k6';

export let options = {
    scenarios: {
        sync_test: {
            executor: 'ramping-vus',
            startVUs: 100,
            exec: 'syncTest',
            stages: [
                { duration: '1m', target: 200 }, // ramp up
                { duration: '3m', target: 200 }, // stable
                { duration: '1m', target: 800 }, // ramp up
                { duration: '3m', target: 800 }, // stable
                { duration: '1m', target: 1000 }, // ramp up
                { duration: '3m', target: 1000 }, // stable
                { duration: '3m', target: 0 }, // ramp-down to 0 users
            ]
        },
        async_test: {
            executor: 'ramping-vus',
            startVUs: 100,
            exec: 'asyncTest',
            stages: [
                { duration: '1m', target: 200 }, // ramp up
                { duration: '3m', target: 200 }, // stable
                { duration: '1m', target: 800 }, // ramp up
                { duration: '3m', target: 800 }, // stable
                { duration: '1m', target: 1000 }, // ramp up
                { duration: '3m', target: 1000 }, // stable
                { duration: '3m', target: 0 }, // ramp-down to 0 users
            ]
        },
    }
};

// Синхронный тест
export function syncTest() {
    let res = http.get('http://sync-app:8080/sync-work', {
      tags: { app: 'sync' }, // Добавляем метку для синхронного приложения
    });
    sleep(1);
  }

  // Асинхронный тест
  export function asyncTest() {
    let res = http.get('http://async-app:8080/async-work', {
      tags: { app: 'async' }, // Добавляем метку для асинхронного приложения
    });
    sleep(1);
  }
