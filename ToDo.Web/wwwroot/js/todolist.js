$(function () {
    // here we will put the code of our application

    //function randomString() generates ID number for cards and columns
    function randomString() {
        var chars = '0123456789abcdefghiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXTZ';
        var str = '';
        for (var i = 0; i < 10; i++) {
            str += chars[Math.floor(Math.random() * chars.length)];
        }
        return str;
    }

    // Column class
    function Column(name) {
        var self = this;
        this.id = randomString();
        this.name = name;
        this.$element = createColumn();

        function createColumn() {
            // components of column
            var $column = $('<div>').addClass('column');
            var $columnTitle = $('<h2>').addClass('column-title').text(self.name);
            var $columnDelete = $('<button>').addClass('btn delete');
            var $columnCardList = $('<ul>').addClass('column-card-list');
            var $columnAddCard = $('<button>').addClass('btn add-card').text('Add a card');

            //events for column
            $columnDelete.click(function () {
                self.removeColumn();
            });
            $columnAddCard.click(function () {
                self.addCard(new Card(prompt('Enter the name of the card')));
            });

            // construction of column components
            $column.append($columnTitle)
                .append($columnAddCard)
                .append($columnDelete)
                .append($columnCardList);

            // return of created column
            return $column;
        }
    }

    Column.prototype = {
        addCard: function (card) {
            this.$element.children('ul').append(card.$element);
        },
        removeColumn: function () {
            this.$element.remove();
        }
    };

    function Card(description) {
        var self = this;

        this.id = randomString();
        this.description = description;
        this.$element = createCard();


        function createCard() {
            // creating cards
            var $card = $('<li>').addClass('card');
            var $cardDescription = $('<p>').addClass('card-description').text(self.description);
            var $cardDelete = $('<button>').addClass('btn delete');

            // event for card - removing it
            $cardDelete.click(function () {
                self.removeCard();
            });

            // combaining the blocks
            $card.append($cardDelete)
                .append($cardDescription);

            // returning the card
            return $card;

        }
    }

    Card.prototype = {
        removeCard: function () {
            this.$element.remove();
        }
    };

    var board = {
        name: 'Kanban Board',
        addColumn: function (column) {
            this.$element.append(column.$element);
            initSortable();
        },
        $element: $('#board .column-container')
    };

    function initSortable() {
        $('.column-card-list').sortable({
            connectWith: '.column-card-list',
            placeholder: 'card-placeholder'
        }).disableSelection();
    }


    $('.create-column')
        .click(function () {
            var name = prompt('Enter a column name');
            var column = new Column(name);
            board.addColumn(column);
        });

    // CREATING COLUMNS
    var todoColumn = new Column('To do');
    var doingColumn = new Column('Doing');
    var doneColumn = new Column('Done');

    // ADDING COLUMNS TO THE BOARD
    board.addColumn(todoColumn);
    board.addColumn(doingColumn);
    board.addColumn(doneColumn);

    // CREATING CARDS
    var card1 = new Card('New task');
    var card2 = new Card('Create kanban boards');

    // ADDING CARDS TO COLUMNS
    todoColumn.addCard(card1);
    doingColumn.addCard(card2);


    console.log(randomString());

});