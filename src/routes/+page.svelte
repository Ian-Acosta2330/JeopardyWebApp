<title>JeopardyWebApp</title>

<script>
    import Taskbar from '/src/components/taskbar.svelte';
    import QuestionDisplay from '/src/components/QuestionDisplay.svelte'; // Correctly importing the component
    import { onMount } from 'svelte';

    const items = [];

    let userInput = '';
    let result = '';

    // Initialize the question data
    let questions = {
        geography: [],
        math: [],
        history: []
    };

    let selectedQuestion = null; // Store the currently selected question

    // Function to handle question selection
    function selectQuestion(category, index) {
        selectedQuestion = questions[category][index];  // Set the selected question based on the category and index
    }

    // Fetch questions on mount
    onMount(async () => {
        try {
            const response = await fetch('http://localhost:5041/jeopardy');  // Fetch from the API
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json(); // Get the JSON data
            questions = data;  // Assign the data to `questions`
        } catch (error) {
            console.error('Error fetching questions:', error);  // Handle any errors
        }
    });

    // Handle answer submission
    function handleSubmit() {
        if (userInput.trim().toLowerCase() === selectedQuestion.answer.toLowerCase()) {  // Compare the answer ignoring case
            alert("Correct!");
        } else {
            alert("Incorrect! The correct answer is: " + selectedQuestion.answer);
        }
        selectedQuestion = null; // Reset the selected question after submission
        userInput = ""; // Clear the input
    }
</script>
<Taskbar {items} />

<h1 class="heading"> Jeopardy Main </h1>

<div class="prompts">
    <div class="container">
        <table>
            <thead>
              <tr>
                <th>Geography</th>
                <th>Math</th>
                <th>History</th>
              </tr>
            </thead>
            <tbody>
              {#each Array(Math.max(questions.geography.length, questions.math.length, questions.history.length)) as _, i}
              <tr>
                <td>
                  {#if selectedQuestion !== questions.geography[i]}
                    <!-- Only show points if the question is not selected -->
                    <button on:click={() => selectQuestion('geography', i)}>
                      {questions.geography[i]?.points}
                    </button>
                  {:else}
                    <!-- Show the QuestionDisplay component when selected -->
                    <QuestionDisplay category="geography" questionData={questions.geography[i]} />
                  {/if}
                </td>
                <td>
                  {#if selectedQuestion !== questions.math[i]}
                    <button on:click={() => selectQuestion('math', i)}>
                      {questions.math[i]?.points}
                    </button>
                  {:else}
                    <QuestionDisplay category="math" questionData={questions.math[i]} />
                  {/if}
                </td>
                <td>
                  {#if selectedQuestion !== questions.history[i]}
                    <button on:click={() => selectQuestion('history', i)}>
                      {questions.history[i]?.points}
                    </button>
                  {:else}
                    <QuestionDisplay category="history" questionData={questions.history[i]} />
                  {/if}
                </td>
              </tr>
              {/each}
            </tbody>
        </table>
    </div>
</div>
