#include<bits/stdc++.h>

using namespace std;

#define WALL '#'

bool bfs(const vector<vector<char>>& grid, 
         const pair<int, int> target,
         vector<vector<bool>>& visited, 
         queue<pair<int,int>>& searchTree, 
         stack<pair<int,int>>& truePath,
         int i, 
         int j){


    queue<pair<int, int>> traversalOrder;

    traversalOrder.push(make_pair(i,j));

    while(!traversalOrder.empty()){
        
        pair<int,int> cur = traversalOrder.front();
        traversalOrder.pop();

        int i = cur.first,
            j = cur.second;
        
        if(!visited[i][j]){

            visited[i][j] = true;

            searchTree.push(make_pair(i,j));
            
            if(i == target.first && j == target.second){
                return true;
            }


            int moves[4][2] = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};

            for (int k = 0; k < 4; k++)
            {
                int new_i = i + moves[k][0],
                    new_j = j + moves[k][1];

                if (new_i >= 0 && new_i < grid.size() && new_j >= 0 && new_j < grid[0].size()){
                    if(grid[new_i][new_j] != WALL){
                        traversalOrder.push(make_pair(new_i,new_j));
                    }
                }
                
            }
        }
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
    stack<pair<int,int>> truePath;

    if(bfs(grid, targetPos, visited, searchTree, truePath, startPos.first, startPos.second)){
        while (!searchTree.empty())
        {
            pair<int, int> pos = searchTree.front();
            searchTree.pop();
            cout << pos.first << " " << pos.second << endl;
        }
              
    }

    return 0;
}