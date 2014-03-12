/// <reference path="../Scripts/jasmine.js" />

test("Will create an object", function () {
    var number = 5;

    equal(5, number);
});

describe('Evaluating a number ', function () {

    var number;
    beforeEach(function() {
        number = 5;
    });

    it('should instantiate a number', function () {
        expect(number).toEqual(5);
    });
});