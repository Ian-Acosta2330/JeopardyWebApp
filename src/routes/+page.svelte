<title>JeopardyWebApp</title>

<style>
    .heading{
        text-align: center;
        font-size: 50px;
    }

</style>

<script>
    import Taskbar from '/src/components/taskbar.svelte';
    import { onMount } from 'svelte';

    const items = [
        {name: 'Home', href: '/routes'},
        {name: 'Game', href: '/game/'},
        {name: 'Profile', href: '/'},
        {name: 'Settings', href: '/'},
        {name: 'Logout', href:'/'}
    ];

    let userInput ='';
    let result='';
    let questions = '';
    let answer = '';
    let category = '';
    
    onMount(async () => {
        try {
            const response = await fetch('http://localhost:5041/jeopardy');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            questions = data.chosenQuestion;
            answer = data.chosenAnswer;     
            category = data.chosenCategory;
        } catch (error) {
            console.error('Error fetching question:', error);
        }
    });

    async function handleSubmit() {
        try {
            if (userInput.trim() === answer) {
                result = 'Correct!';
            } else {
                result = 'Incorrect!';
            }
            setTimeout(function() {
                location.reload()}, 1000);
        } catch (error) {
            console.error('Could not get error', error);
        } finally {
            userInput = '';
        }
    }
    
</script>

<Taskbar {items}/>

<h1 class="heading"> Jeopardy Main </h1>

<div class="prompts">
    <h1>Category: {category} </h1>
    <h1>Jeopardy Question: {questions} ? </h1>

    <form on:submit|preventDefault={handleSubmit}>
        <input type="text" bind:value={userInput} placeholder="Your Answer Here"/>
        <button id="answerSubmit" type="submit">Submit</button>
    </form>

    <div id="finalResult">{result}</div>

</div>