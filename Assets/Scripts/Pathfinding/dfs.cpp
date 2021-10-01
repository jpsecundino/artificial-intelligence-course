#include<vector>
#include<queue>
#include<tuple>
#include<iostream>

using namespace std;
#define WALL '#'

bool dfs(const vector<vector<char>>& grid, 
         const pair<int, int> target,
         vector<vector<bool>>& visited, 
         queue<pair<int,int>>& searchTree, 
         int i, 
         int j){

    if (i < 0 || i >= grid.size() || j < 0 || j >= grid[0].size())
        return false;

    if(visited[i][j] || grid[i][j] == WALL)
        return false;

    visited[i][j] = true;

    searchTree.push(make_pair(i, j));

    if(i == target.first && j == target.second)
        return true;

    int moves[4][2] = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};

    for (int k = 0; k < 4; k++)
    {
        if(dfs(grid, target, visited, searchTree, i + moves[k][0], j + moves[k][1])){
            return true;
        }
        
        if(make_pair(i,j) != searchTree.back())
            searchTree.push(make_pair(i, j));
    }

    return false;
}

int main(){
    int n, m;

    cin >> n >> m;

    vector<vector<char>> grid(n, vector<char>(m));
    vector<vector<bool>> visited(n, vector<bool>(m, false));

    pair<int, int> startPos;
    pair<int, int> targetPos;

    for (int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++){
            cin >> grid[i][j];

            if(grid[i][j] == 's'){
                startPos = make_pair(i, j);
            } 

            if(grid[i][j] == 't'){
                targetPos = make_pair(i, j);
            } 
        }

    }

    queue<pair<int,int>> searchTree;

    if(dfs(grid, targetPos, visited, searchTree, startPos.first, startPos.second)){
        while (!searchTree.empty())
        {
            pair<int, int> pos = searchTree.front();
            searchTree.pop();
            cout << pos.first << " " << pos.second << endl;
        }
    }

    return 0;
}