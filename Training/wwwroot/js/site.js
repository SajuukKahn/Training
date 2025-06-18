document.body.style.userSelect = 'none';

document.addEventListener("dragover", (event) => {
    event.preventDefault();
});

document.addEventListener("drop", (event) => {
    event.preventDefault();
});

document.querySelectorAll('.problem').forEach(problem => {
    const hints = [...problem.querySelectorAll('.hint')];
    const answer = problem.querySelector('.answer');

    // Disable all hints except the first
    hints.forEach((hint, index) => {
        if (index === 0) {
            hint.style.display = 'block'
            hint.querySelector('summary').style.listStyleType = 'square';
        }
        if (index > 0) {
            hint.querySelector('summary').style.pointerEvents = 'none';
            hint.querySelector('summary').style.listStyleType = 'none';
            hint.style.display = 'none'
            hint.dataset.locked = 'true'; // used to prevent user interaction
        }
    });

    // Disable answer
    if (answer) {
        answer.querySelector('summary').style.pointerEvents = 'none';
        answer.querySelector('summary').style.listStyleType = 'none';
        answer.style.display = 'none'
        answer.dataset.locked = 'true';
    }

    // Attach event listeners
    hints.forEach((hint, index) => {
        hint.addEventListener('toggle', function handler(e) {
            if (hint.open && hint.dataset.locked !== 'true') {
                // Lock this hint open
                hint.setAttribute('open', '');
                hint.querySelector('summary').style.pointerEvents = 'none';
                hint.querySelector('summary').style.listStyleType = 'none';
                hint.dataset.locked = 'true';

                // Enable next hint
                if (hints[index + 1]) {
                    const next = hints[index + 1];
                    next.style.display = 'block'
                    next.querySelector('summary').style.pointerEvents = 'auto';
                    next.querySelector('summary').style.listStyleType = 'square';
                    next.dataset.locked = 'false';
                } else {
                    // All hints opened, enable answer
                    if (answer) {
                        answer.style.display = 'block'
                        answer.querySelector('summary').style.pointerEvents = 'auto';
                        answer.querySelector('summary').style.listStyleType = 'square';
                        answer.dataset.locked = 'false';
                    }
                }
            } else if (!hint.open) {
                // prevent re-closing by immediately re-opening it
                if (hint.dataset.locked === 'true') {
                    hint.setAttribute('open', '');
                }
            }
        });
    });
});