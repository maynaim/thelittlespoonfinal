// Pie Chart Category

function drawPieChart(data) {
    var totalCount = 'מתכונים לפי\n קטגוריה';		//calcuting total manually

    var width = 540,
        height = 540,
        radius = 200;

    var arc = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(100);

    var pie = d3.pie()
        .sort(null)
        .value(function (d) {
            return d.count;
        });

    var svg = d3.select('#aPc').append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var g = svg.selectAll(".arc")
        .data(pie(data))
        .enter().append("g");

    g.append("path")
        .attr("d", arc)
        .style("fill", function (d, i) {
            return d.data.color;
        });

    g.append("text")
        .attr("transform", function (d) {
            var _d = arc.centroid(d);
            _d[0] *= 1.5;	//multiply by a constant factor
            _d[1] *= 1.5;	//multiply by a constant factor
            return "translate(" + _d + ")";
        })
        .attr("dy", ".50em")
        .style("text-anchor", "middle")
        .text(function (d) {
            return d.data.label;
        });

    g.append("text")
        .attr("text-anchor", "middle")
        .attr('font-size', '1em')
        .attr('y', 20)
        .text(totalCount);
}

$.getJSON("/Statisticss/Articles", null, function (data) {
    drawPieChart(data);
});

function drawPieChartComments(data) {
    var totalCount = 'תגובות לפי\n קטגוריה';		//calcuting total manually

    var width = 350,
        height = 350,
        radius = 100;

    var arc = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(100);

    var pie = d3.pie()
        .sort(null)
        .value(function (d) {
            return d.count;
        });

    var svg = d3.select('#cPc').append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var g = svg.selectAll(".arc")
        .data(pie(data))
        .enter().append("g");

    g.append("path")
        .attr("d", arc)
        .style("fill", function (d, i) {
            return d.data.color;
        });

    g.append("text")
        .attr("transform", function (d) {
            var _d = arc.centroid(d);
            _d[0] *= 1.5;	//multiply by a constant factor
            _d[1] *= 1.5;	//multiply by a constant factor
            return "translate(" + _d + ")";
        })
        .attr("dy", ".50em")
        .style("text-anchor", "middle")
        .text(function (d) {
            return d.data.label;
        });

    g.append("text")
        .attr("text-anchor", "middle")
        .attr('font-size', '1em')
        .attr('y', 20)
        .text(totalCount);
}

$.getJSON("/Statisticss/Comments", null, function (data) {
    drawPieChartComments(data);
});

// Line Date chart - recipes

function drawLineChart(data) {
    var lineData = [];
    lineData = data.map(d => { return { date: new Date(d.dateDate.year, d.dateDate.month - 1, d.dateDate.day), nps: d.nps } });

    lineData.sort(function (a, b) {
        return new Date(b.date) - new Date(a.date);
    });

    var height = 200;
    var width = 800;
    var hEach = 40;

    var margin = { top: 20, right: 15, bottom: 25, left: 25 };

    width = width - margin.left - margin.right;
    height = height - margin.top - margin.bottom;

    var svg = d3.select('#aDc').append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    // set the ranges
    var x = d3.scaleTime().range([0, width]);

    x.domain(d3.extent(lineData, function (d) { return d.date; }));


    var y = d3.scaleLinear().range([height, 0]);


    y.domain([d3.min(lineData, function (d) { return d.nps; }) - 5, 100]);

    var valueline = d3.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.nps); })
        .curve(d3.curveMonotoneX);

    svg.append("path")
        .data([lineData])
        .attr("class", "line")
        .attr("d", valueline);

    //  var xAxis_woy = d3.axisBottom(x).tickFormat(d3.timeFormat("Week %V"));
    var xAxis_woy = d3.axisBottom(x).ticks(11).tickFormat(d3.timeFormat("%y-%b-%d")).tickValues(lineData.map(d => d.date));

    svg.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis_woy);

    //  Add the Y Axis
    //  svg.append("g").call(d3.axisLeft(y));

    svg.selectAll(".dot")
        .data(lineData)
        .enter()
        .append("circle") // Uses the enter().append() method
        .attr("class", "dot") // Assign a class for styling
        .attr("cx", function (d) { return x(d.date) })
        .attr("cy", function (d) { return y(d.nps) })
        .attr("r", 5);


    svg.selectAll(".text")
        .data(lineData)
        .enter()
        .append("text") // Uses the enter().append() method
        .attr("class", "label") // Assign a class for styling
        .attr("x", function (d, i) { return x(d.date) })
        .attr("y", function (d) { return y(d.nps) })
        .attr("dy", "-5")
        .text(function (d) { return d.nps; });

    svg.append('text')
        .attr('x', 10)
        .attr('y', -5)
        .text('מתכונים לפי יום'); 
}

$.getJSON("/Statisticss/ArticlesCreation", null, function (data) {
    drawLineChart(data);
});
