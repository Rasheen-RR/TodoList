function swInstall(event) {
    console.info("installing")
    event.waitUntil(
        caches.open('todov1').then((cache) => {
            return cache.addAll([
                './',
                './css/site.css',
                './js/site.js',
                './favicon.ico',
                './lib/bootstrap/dist/js/bootstrap.bundle.min.js',
                './lib/jquery/dist/jquery.min.js',
                './lib/bootstrap/dist/css/bootstrap.min.css ',

            ])
        })
    )
}

self.addEventListener('install', swInstall);

self.addEventListener('fetch', function (event) {
    event.respondWith(
        caches.match(event.request).then(function (cacheResponse) {
            return cacheResponse || fetch(event.request)
        }).catch(() => {
            console.log("off")
        })
    )
})