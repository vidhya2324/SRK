// SRK Fruits India — site.js
// Vanilla JS only (no framework/CDN dependency) so the static site
// stays lightweight and easy to host anywhere.
(function () {
    "use strict";

    document.addEventListener("DOMContentLoaded", function () {
        initPageCurtain();
        initHeaderScroll();
        initMobileNav();
        initScrollReveal();
        initCounters();
    });

    // ---- Page load curtain -------------------------------------------------
    function initPageCurtain() {
        var curtain = document.getElementById("pageCurtain");
        if (!curtain) return;

        window.requestAnimationFrame(function () {
            setTimeout(function () {
                curtain.classList.add("curtain-open");
                setTimeout(function () {
                    curtain.classList.add("curtain-done");
                }, 950);
            }, 250);
        });
    }

    // ---- Header shrink on scroll -------------------------------------------
    function initHeaderScroll() {
        var header = document.getElementById("siteHeader");
        if (!header) return;

        function onScroll() {
            if (window.scrollY > 30) {
                header.classList.add("scrolled");
            } else {
                header.classList.remove("scrolled");
            }
        }
        onScroll();
        window.addEventListener("scroll", onScroll, { passive: true });
    }

    // ---- Mobile nav toggle ---------------------------------------------------
    function initMobileNav() {
        var toggle = document.getElementById("navToggle");
        var nav = document.getElementById("mainNav");
        if (!toggle || !nav) return;

        toggle.addEventListener("click", function () {
            var isOpen = nav.classList.toggle("open");
            toggle.classList.toggle("open", isOpen);
            toggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
        });

        nav.querySelectorAll("a").forEach(function (link) {
            link.addEventListener("click", function () {
                nav.classList.remove("open");
                toggle.classList.remove("open");
                toggle.setAttribute("aria-expanded", "false");
            });
        });
    }

    // ---- Scroll reveal (IntersectionObserver) --------------------------------
    function initScrollReveal() {
        var targets = document.querySelectorAll(".reveal-up, .reveal-left, .reveal-right");
        if (!targets.length) return;

        if (!("IntersectionObserver" in window)) {
            targets.forEach(function (el) { el.classList.add("in-view"); });
            return;
        }

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("in-view");
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.15, rootMargin: "0px 0px -60px 0px" });

        targets.forEach(function (el) { observer.observe(el); });
    }

    // ---- Animated stat counters ------------------------------------------------
    function initCounters() {
        var counters = document.querySelectorAll(".stat-num[data-count]");
        if (!counters.length) return;

        function animateCounter(el) {
            var target = parseInt(el.getAttribute("data-count"), 10) || 0;
            var duration = 1400;
            var start = null;

            function step(timestamp) {
                if (!start) start = timestamp;
                var progress = Math.min((timestamp - start) / duration, 1);
                var eased = 1 - Math.pow(1 - progress, 3); // ease-out-cubic
                el.textContent = Math.floor(eased * target);
                if (progress < 1) {
                    window.requestAnimationFrame(step);
                } else {
                    el.textContent = target;
                }
            }
            window.requestAnimationFrame(step);
        }

        if (!("IntersectionObserver" in window)) {
            counters.forEach(animateCounter);
            return;
        }

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    animateCounter(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.5 });

        counters.forEach(function (el) { observer.observe(el); });
    }
})();
